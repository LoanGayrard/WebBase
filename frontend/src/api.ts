export const API_BASE_URL =
  import.meta.env.VITE_API_BASE_URL ?? "http://localhost:8080";

export async function getTodos() {
  const res = await fetch(`${API_BASE_URL}/api/todos`, {
    headers: { Accept: "application/json" },
  });

  if (!res.ok) {
    const txt = await res.text();
    throw new Error(`GET /api/todos failed (${res.status}): ${txt}`);
  }

  return (await res.json()) as Array<{ id: number; title: string; done: boolean }>;
}
