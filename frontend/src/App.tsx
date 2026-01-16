import { Link, Route, Routes } from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Register";
import { useAuth } from "./context/AuthContext";

function Home() {
  const { user, loading, logout } = useAuth();

  if (loading) return <div style={{ padding: 20 }}>Chargement...</div>;

  return (
    <div style={{ padding: 20, fontFamily: "system-ui" }}>
      <h1>Home</h1>

      <p>
        Statut: {user ? `connecté (${user.email ?? "user"})` : "non connecté"}
      </p>

      <div style={{ display: "flex", gap: 12 }}>
        <Link to="/login">Login</Link>
        <Link to="/register">Register</Link>
        {user && (
          <button onClick={logout} style={{ marginLeft: 12 }}>
            Logout
          </button>
        )}
      </div>
    </div>
  );
}

export default function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
    </Routes>
  );
}
