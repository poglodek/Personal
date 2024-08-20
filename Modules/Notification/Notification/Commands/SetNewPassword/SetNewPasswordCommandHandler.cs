using MediatR;
using Notification.Templates.User.SetUserPassword;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Notification.Commands.SetNewPassword;

public class SetNewPasswordCommandHandler : IRequestHandler<SetNewPasswordCommand, Unit>
{
    private readonly ISendGridClient _sendGridClient;
    private readonly NotificationOptions _options;

    public SetNewPasswordCommandHandler(ISendGridClient sendGridClient, NotificationOptions options)
    {
        _sendGridClient = sendGridClient;
        _options = options;
    }
    
    public async Task<Unit> Handle(SetNewPasswordCommand request, CancellationToken cancellationToken)
    {
        var template = new SetUserPasswordTemplate().GetReadyTemplate(new SetUserPasswordTemplateData(request.Url));
        
        var message = new SendGridMessage
        {
            From = new EmailAddress(_options.Mail, _options.From),
            Subject = "Set new password - Personal App",
            HtmlContent = template,
        };
        message.AddTo(new EmailAddress(request.Mail));
        
        await _sendGridClient.SendEmailAsync(message, cancellationToken);
        
        return Unit.Value;
    }
}