import { useEffect, useState } from "react";
import { getTodos } from "./api";

type Todo = { id: number; title: string; done: boolean };

export default function Todos() {
  const [todos, setTodos] = useState<Todo[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    (async () => {
      try {
        setLoading(true);
        setError(null);
        const data = await getTodos();
        setTodos(data);
      } catch (e: any) {
        setError(e?.message ?? "Unknown error");
      } finally {
        setLoading(false);
      }
    })();
  }, []);

  return (
    <div style={{ padding: 24, fontFamily: "system-ui, sans-serif" }}>
      <h1>WebBase</h1>

      {loading && <p>Loading...</p>}
      {error && (
        <pre style={{ padding: 12, background: "#111", color: "#eee", borderRadius: 8 }}>
          {error}
        </pre>
      )}

      {!loading && !error && (
        <ul>
          {todos.map((t) => (
            <li key={t.id}>
              <span style={{ textDecoration: t.done ? "line-through" : "none" }}>
                {t.title}
              </span>{" "}
              {t.done ? "✅" : ""}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
