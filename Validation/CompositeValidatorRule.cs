﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace Amica.Validation
{
public class CompositeValidatorRule : IValidationRule
{
    private IValidator[] _validators;

    public CompositeValidatorRule(params IValidator[] validators)
    {
        _validators = validators;
    }

    #region IValidationRule Members
    public string RuleSet
    {
        get; set;
    }

    public IEnumerable<ValidationFailure> Validate(ValidationContext context)
    {
        var ret = new List<ValidationFailure>();

        foreach(var v in _validators)
        {
            ret.AddRange(v.Validate(context).Errors);
        }

        return ret;
    }

        public Task<IEnumerable<ValidationFailure>> ValidateAsync(ValidationContext context, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public void ApplyCondition(Func<object, bool> predicate, ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators)
        {
            throw new NotImplementedException();
        }

        public void ApplyAsyncCondition(Func<object, Task<bool>> predicate, ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators)
        {
            throw new NotImplementedException();
        }

        public void ApplyCondition(Func<PropertyValidatorContext, bool> predicate, ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators)
        {
            throw new NotImplementedException();
        }

        public void ApplyAsyncCondition(Func<PropertyValidatorContext, CancellationToken, Task<bool>> predicate, ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FluentValidation.Validators.IPropertyValidator> Validators
    {
        get { yield break; }
    }

        public string[] RuleSets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion
    }

}
