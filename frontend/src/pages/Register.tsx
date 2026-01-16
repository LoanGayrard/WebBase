import { useState } from "react";
import { register as apiRegister } from "../api/auth.api";
import { useAuth } from "../context/AuthContext";

export default function Register() {
  const { login } = useAuth();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [busy, setBusy] = useState(false);
  const [ok, setOk] = useState(false);

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setOk(false);
    setBusy(true);

    try {
      await apiRegister({ email, password });
      // Optionnel: auto-login après register
      await login(email, password);
      setOk(true);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Register error");
    } finally {
      setBusy(false);
    }
  };

  return (
    <div style={{ maxWidth: 420, margin: "40px auto", fontFamily: "system-ui" }}>
      <h1>Register</h1>

      <form onSubmit={onSubmit} style={{ display: "grid", gap: 12 }}>
        <label>
          Email
          <input
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            type="email"
            required
            style={{ width: "100%", padding: 10, marginTop: 6 }}
          />
        </label>

        <label>
          Password
          <input
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            type="password"
            required
            style={{ width: "100%", padding: 10, marginTop: 6 }}
          />
        </label>

        <button disabled={busy} type="submit" style={{ padding: 10 }}>
          {busy ? "Création..." : "Créer un compte"}
        </button>

        {ok && <div style={{ color: "green" }}>Compte créé avec succès !</div>}
        {error && <div style={{ color: "crimson" }}>{error}</div>}
      </form>
    </div>
  );
}
