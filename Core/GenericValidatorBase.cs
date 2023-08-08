using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericValidator.Core;

public class GenericValidatorBase : AbstractValidator<object>, IGenericValidatorBase
{
    private GenericValidatorBase()
    {
    }

    public static IGenericValidatorBase CreateGenericValidator()
        => new GenericValidatorBase();

    public ValidationResult ValidadeProperties(params string[] properties)
        => this.Validate(
            new object(),
            options => options.IncludeProperties(properties));

    public ValidationResult ValidateRules()
        => Validate(new object());

    public ValidationResult ValidateRuleSets(params string[] ruleSets)
        => this.Validate(
            new object(),
            options => options.IncludeRuleSets(ruleSets));

    public ValidationResult RunValidator()
        => base.Validate(new object());

    IGenericValidatorBase IGenericValidatorBase.AddRule<T>
        (T input, Action<IRuleBuilderInitial<object, T>> action)
    {
        action(RuleFor(x => input));

        return this;
    }

    IGenericValidatorBase IGenericValidatorBase.AddRule<T>
        (T input, Func<object, bool> condition, Action action)
    {
        When(condition, action);

        return this;
    }

    public IGenericValidatorBase AddRuleSet<T>(string ruleSetName, Action<IGenericValidatorBase> action)
    {
        RuleSet(ruleSetName, () =>
        {
            action(this);
        });

        return this;
    }
}
