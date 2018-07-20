﻿using System;
using Amica.Models;
using FluentValidation;

namespace Amica.Validation
{
    public class ContactValidator : AbstractValidator<Contact>
    {
		public ContactValidator()
        {
            RuleFor(contact => contact.Name).NotEmpty();

            RuleFor(contact => contact.VatIdentificationNumber)
                .Must(ValidatorHelpers.BeValidVatNumber)
                .WithMessage(ErrorMessages.VatIdentificationNumber)
                .When(contact => contact.VatIdentificationNumber != null);
		    RuleFor(contact => contact.TaxIdentificationNumber)
                .Must(ValidatorHelpers.BeValidTaxIdNumber)
                .WithMessage(ErrorMessages.TaxIdentificationNumber)
                .When(c => c.TaxIdentificationNumber != null);

            RuleFor(contact => contact.PublicAdministrationIndex).Length(6);

            // TODO WithMessage, localized
            RuleFor(contact => contact.Relationship).Must(BeValidContactKind);

            RuleFor(contact => contact.Address).SetValidator(new AddressExValidator());

            RuleFor(contact => contact.OtherAddresses).
                SetCollectionValidator(new ShippingAddressValidator());
        }

		private static bool BeValidContactKind(Relationship relationship)
        {
            return !(
                relationship.IsClient == false && 
				relationship.IsVendor == false &&
				relationship.IsCourier == false &&
				relationship.IsAgent== false &&
				relationship.IsAreaManager == false
				);
        }

    }
}
