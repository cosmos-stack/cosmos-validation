using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May register for custom validator interface <br />
    /// 标记可注册自定义验证器
    /// </summary>
    public interface IMayRegisterForCustomValidator
    {
        /// <summary>
        /// Register for custom validator <br />
        /// 注册指定类型的自定义验证器
        /// </summary>
        /// <typeparam name="TValidator"></typeparam>
        /// <returns></returns>
        IValidationRegistrar ForCustomValidator<TValidator>() where TValidator : CustomValidator, new();
        
        /// <summary>
        /// Register for custom validator <br />
        /// 注册指定类型的自定义验证器
        /// </summary>
        /// <typeparam name="TValidator"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidationRegistrar ForCustomValidator<TValidator, T>() where TValidator : CustomValidator<T>, new();
        
        /// <summary>
        /// Register for custom validator <br />
        /// 注册指定类型的自定义验证器
        /// </summary>
        /// <param name="validator"></param>
        /// <returns></returns>
        IValidationRegistrar ForCustomValidator(CustomValidator validator);
        
        /// <summary>
        /// Register for custom validator <br />
        /// 注册指定类型的自定义验证器
        /// </summary>
        /// <param name="validator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidationRegistrar ForCustomValidator<T>(CustomValidator<T> validator);
    }

    /// <summary>
    /// May register for custom validator continue interface <br />
    /// 标记可继续注册自定义验证器
    /// </summary>
    public interface IMayContinueRegisterForCustomValidator
    {
        /// <summary>
        /// Register for custom validator <br />
        /// 注册指定类型的自定义验证器
        /// </summary>
        /// <typeparam name="TValidator"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar AndForCustomValidator<TValidator>() where TValidator : CustomValidator, new();
        
        /// <summary>
        /// Register for custom validator <br />
        /// 注册指定类型的自定义验证器
        /// </summary>
        /// <typeparam name="TValidator"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar AndForCustomValidator<TValidator, T>() where TValidator : CustomValidator<T>, new();
        
        /// <summary>
        /// Register for custom validator <br />
        /// 注册指定类型的自定义验证器
        /// </summary>
        /// <param name="validator"></param>
        /// <returns></returns>
        IFluentValidationRegistrar AndForCustomValidator(CustomValidator validator);
        
        /// <summary>
        /// Register for custom validator <br />
        /// 注册指定类型的自定义验证器
        /// </summary>
        /// <param name="validator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar AndForCustomValidator<T>(CustomValidator<T> validator);
    }
}