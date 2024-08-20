using Portfolio.Management.Domain.Enums;

namespace Portfolio.Management.Domain.Dtos.Response
{
    public record ErrorResponse
    (
       ErrorCode ErrorCode,
       string Message
    );
}