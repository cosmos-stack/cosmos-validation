namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May take effect interface <br />
    /// 标记可生效
    /// </summary>
    public interface IMayTakeEffect
    {
        /// <summary>
        /// Take effect <br />
        /// 生效
        /// </summary>
        void TakeEffect();
        
        /// <summary>
        /// Take effect and back <br />
        /// 生效并返回
        /// </summary>
        /// <returns></returns>
        IValidationRegistrar TakeEffectAndBack();
    }
}