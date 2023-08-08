using FluentValidation;
using FluentValidation.Results;

namespace GenericValidator.Core
{
    public interface IGenericValidatorBase
    {
        IGenericValidatorBase AddRule<T>
            (T input, Action<IRuleBuilderInitial<object, T>> action);

        IGenericValidatorBase AddRule<T>
            (T input, Func<object, bool> condition, Action action);

        IGenericValidatorBase AddRuleSet
        (string ruleSetName, Action action);

        ValidationResult RunValidator();

        ValidationResult ValidateRules();

        ValidationResult ValidateRuleSets(params string[] ruleSets);

        ValidationResult ValidadeProperties(params string[] properties);
    }
}