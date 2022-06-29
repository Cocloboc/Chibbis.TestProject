using System;
using FluentValidation;

namespace Chibbis.TestProject.CartService.Application.Consumers.Webhook.Commands.SetWebhook
{
    public class SetWebhookValidator: AbstractValidator<SetWebhookCommand>
    {
        public SetWebhookValidator()
        {
            RuleFor(x => x.WebhookUrl)
                .NotEmpty();
            RuleFor(x => x.WebhookUrl)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.WebhookUrl))
                .WithMessage("Incorrect Url");
        }
    }
}