using MediatR;
using Notification.Templates.User.ActivateUser;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Notification.Commands.ActivateUser;

public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand,Unit>
{
    private readonly ISendGridClient _sendGridClient;
    private readonly NotificationOptions _options;

    public ActivateUserCommandHandler(ISendGridClient sendGridClient, NotificationOptions options)
    {
        _sendGridClient = sendGridClient;
        _options = options;
    }
    
    public async Task<Unit> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var template = new ActivateUserTemplate().GetReadyTemplate(new ActivateUserTemplateData(request.Url));
        
        var message = new SendGridMessage
        {
            From = new EmailAddress(_options.Mail, _options.From),
            Subject = "Activate your account - Personal App",
            HtmlContent = template,
        };
        message.AddTo(new EmailAddress(request.Mail));
        
        await _sendGridClient.SendEmailAsync(message, cancellationToken);
        
        return Unit.Value;
    }
}