using NetKubernates.Models;

namespace NetKubernates.Token;

public interface IJwtGenerator {
    string GenerateToken(Usuario user);
}