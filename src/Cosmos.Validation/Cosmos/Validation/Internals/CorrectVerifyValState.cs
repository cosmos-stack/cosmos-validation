using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Validation.Internals
{
    internal class CorrectVerifyValState
    {
        public CorrectVerifyValState()
        {
            var firstBlock = new CorrectVerifyValBlock();

            Blocks = new() {firstBlock};
            WorkingBlock = firstBlock;
        }

        private List<CorrectVerifyValBlock> Blocks { get; set; }

        private CorrectVerifyValBlock WorkingBlock { get; set; }

        public void Append(CorrectVerifyVal val)
        {
            WorkingBlock.Append(val);
        }

        public void Ops(CorrectVerifyValOps ops)
        {
            if (ops == CorrectVerifyValOps.AndOp)
            {
                return;
            }

            if (ops == CorrectVerifyValOps.AndOpWithNewScope)
            {
                var block = new CorrectVerifyValBlock();
                WorkingBlock.AddBlock(block, true);
                WorkingBlock = block;
            }

            if (ops == CorrectVerifyValOps.OrOp)
            {
                var block = new CorrectVerifyValBlock();
                Blocks.Add(block);
                WorkingBlock = block;
            }

            if (ops == CorrectVerifyValOps.OrOpWithSameScope)
            {
                var block = new CorrectVerifyValBlock();
                WorkingBlock.AddBlock(block, false);
                WorkingBlock = block;
            }
        }

        public void JumpToRoot()
        {
#if NETFRAMEWORK
            WorkingBlock = Blocks[Blocks.Count - 1];
#else
            WorkingBlock = Blocks[^1];
#endif
        }

        public bool IncludeFailures
        {
            get
            {
                foreach (var block in Blocks)
                {
                    if (block.IncludeFailures == false)
                        return false;
                }

                return true;
            }
        }

        public IEnumerable<CorrectVerifyVal> GetCorrectVerifyValSet()
        {
            if (!IncludeFailures)
                return Enumerable.Empty<CorrectVerifyVal>();

            var list = new List<CorrectVerifyVal>();

            foreach (var block in Blocks)
            {
                list.AddRange(block.GetCorrectVerifyValSet());
            }

            return list;
        }
    }

    internal class CorrectVerifyValBlock
    {
        private List<CorrectVerifyVal> ValSet { get; set; }

        private List<CorrectVerifyValBlock> Blocks { get; set; }

        private CorrectVerifyValBlock WorkingBlock { get; set; }

        /// <summary>
        /// 与/或 标记位，用于辅助 Blocks 工作，默认为 true。 <br />
        /// true - AND <br />
        /// false - OR
        /// </summary>
        private bool AndOrFlag { get; set; } = true;

        public CorrectVerifyValBlock Parent { get; set; }

        public bool IncludeFailures
        {
            get
            {
                if (Blocks is null && ValSet is null) return false;
                if (Blocks is null) return ValSet.Any(x => x.IsSuccess == false);
                return AndOrFlag
                    ? Blocks.Any(x => x.IncludeFailures)
                    : Blocks.Any(x => !x.IncludeFailures);
            }
        }

        public void Append(CorrectVerifyVal val)
        {
            if (Blocks is null && val is not null)
            {
                ValSet ??= new List<CorrectVerifyVal>();
                ValSet.Add(val);
            }
            else
            {
                WorkingBlock.Append(val);
            }
        }

        public void AddBlock(CorrectVerifyValBlock block, bool andOrFlag)
        {
            if (block is null) return;

            if (Blocks is null)
            {
                var firstBlock = new CorrectVerifyValBlock {ValSet = ValSet, AndOrFlag = andOrFlag, Parent = this};
                ValSet = null;
                Blocks = new List<CorrectVerifyValBlock> {firstBlock};
                AndOrFlag = andOrFlag;

                block.Parent = this;
                Blocks.Add(block);

                WorkingBlock = block;
            }
            else
            {
                if (AndOrFlag == false && andOrFlag == false || AndOrFlag == true && andOrFlag == true)
                {
                    //0 && 0 || 1 && 1
                    block.Parent = this;
                    Blocks.Add(block);

                    WorkingBlock = block;
                }
                else if (AndOrFlag == true && andOrFlag == false)
                {
                    //1 && 0
                    var groupedBlock = new CorrectVerifyValBlock {Blocks = Blocks, AndOrFlag = AndOrFlag, Parent = this};
                    Blocks = new List<CorrectVerifyValBlock> {groupedBlock};
                    AndOrFlag = andOrFlag;

                    block.Parent = this;
                    Blocks.Add(block);

                    WorkingBlock = block;
                }
                else
                {
                    //0 && 1
                    WorkingBlock.AddBlock(block, andOrFlag);

                    WorkingBlock = block;
                }
            }
        }

        public IEnumerable<CorrectVerifyVal> GetCorrectVerifyValSet()
        {
            if (!IncludeFailures)
                return Enumerable.Empty<CorrectVerifyVal>();

            if (Blocks is null)
                return ValSet.Where(x => x.IsSuccess == false);

            var list = new List<CorrectVerifyVal>();

            list.AddRange(Blocks.SelectMany(x => x.GetCorrectVerifyValSet()));

            return list;
        }
    }

    internal enum CorrectVerifyValOps
    {
        AndOp, // 默认操作 - AND
        AndOpWithNewScope, // AND，并生成一个新的 Scope，用于记录随后的操作
        OrOp, // OR，将创建一个新的 Scope； 如果之前是 AND 操作，则将 AND 操作打包为一个 Scope
        OrOpWithSameScope // OR 操作，
    }
}