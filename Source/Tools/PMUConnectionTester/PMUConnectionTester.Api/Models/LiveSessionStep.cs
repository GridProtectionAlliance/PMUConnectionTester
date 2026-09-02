namespace ConnectionTester.Api.Models;

/// <summary>
/// Step a live capture session is currently in, or last reached before erroring out.
/// </summary>
public enum LiveSessionStep
{
    Conectando,
    AguardandoComunicacao,
    CapturandoDados,
    EncerrandoCaptura,
    Concluido
}