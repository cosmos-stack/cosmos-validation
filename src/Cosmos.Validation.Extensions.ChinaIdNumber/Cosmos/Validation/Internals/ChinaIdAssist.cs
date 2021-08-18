using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Date;
using Cosmos.Text;
using Cosmos.Validation.Internals.Standards;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Internals
{
    internal abstract class ChinaIdAssist : IAssist
    {
        public abstract bool ValidLength(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info);

        public bool ValidBirthday(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var @try = ValidBirthdayImpl(idNumber, options, failures, out var birthday);
            if (!@try)
            {
                failures.Add(new(options.ParamName, "The date of birth cannot be recognized."));
                return false;
            }
            
            var now = DateTimeFactory.Now().Date;
            @try = birthday > DateTime.MinValue && birthday.Year >= options.MinYear && birthday <= now;
            info.Birthday = birthday;
            
            if (!@try)
            {
                failures.Add(new(options.ParamName, "The date of birth is invalid or exceeds the limit."));
                return false;
            }

            return true;
        }

        public bool ValidArea(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var s = idNumber.Substring(0, 6);
            if (!s.All(c => c.IsNumber()))
            {
                failures.Add(new(options.ParamName, "Invalid administrative area code."));
                return false;
            }

            var areaNumber = int.Parse(s);
            var areaInfo = __getDeepestArea(areaNumber);
            var @try = areaInfo is not null && areaInfo.GetDepth() >= (int)options.Limit;

            info.RecognizableArea = areaInfo;
            info.AreaNumber = areaNumber;

            if (!@try)
            {
                if (areaInfo is null)
                    failures.Add(new(options.ParamName, "Invalid administrative area code."));
                else
                    failures.Add(new VerifyFailure(options.ParamName, $"Administrative area's level is lower than level {options.Limit}"));
                return false;
            }

            return true;

            // ReSharper disable once InconsistentNaming
            ChinaIdAreaInfo __getDeepestArea(int iArea)
            {
                ChinaIdAreaInfo area = null, lastArea = null;
                var d = GBT2260_2013.Singleton.GetDictionary();
                while (iArea > 0)
                {
                    if (iArea < 10)
                        throw new ArgumentException("Administrative code is wrong.");
                    if (d.ContainsKey(iArea))
                    {
                        var t = new ChinaIdAreaInfo(iArea, d[iArea]);
                        if (area is null)
                            area = t;
                        else
                            lastArea.Parent = t;
                        lastArea = t;
                    }

                    iArea /= 100;
                }

                return area;
            }
        }

        public bool ValidCheckBit(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var @try = ValidCheckBitImpl(idNumber, options, failures, out var checkBit)|| options.IgnoreCheckBit;;
            
            if (!@try)
            {
                failures.Add(new(options.ParamName, "Wrong check code."));
                return false;
            }

            info.CheckBit = checkBit;

            return true;
        }

        public bool ValidTheRest(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var strSequence = GetSequenceImpl(idNumber);
            var sequence = int.Parse(strSequence);

            info.Gender = (ChinaIdGender)(sequence % 2);
            info.Sequence = sequence;

            return true;
        }

        protected abstract bool ValidBirthdayImpl(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, out DateTime date);

        protected abstract bool ValidCheckBitImpl(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, out char rightBit);

        protected abstract string GetSequenceImpl(string idNumber);
    }
}