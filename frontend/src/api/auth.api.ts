const API_BASE_URL = import.meta.env.VITE_API_BASE_URL + "/api/auth";

export type RegisterPayload = {
  email: string;
  password: string;
};

export type LoginPayload = {
  email: string;
  password: string;
};

export type MeResponse = {
  email: string;
  userName?: string;
  id?: string;
  roles?: string[];
} | null;

async function readError(res: Response): Promise<string> {
  const text = await res.text();
  return text || `${res.status} ${res.statusText}`;
}

export async function register(payload: RegisterPayload): Promise<void> {
  const res = await fetch(`${API_BASE_URL}/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    credentials: "include",
    body: JSON.stringify(payload),
  });

  if (!res.ok) throw new Error(await readError(res));
}

export async function login(payload: LoginPayload): Promise<void> {
  const res = await fetch(`${API_BASE_URL}/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    credentials: "include",
    body: JSON.stringify(payload),
  });

  if (!res.ok) throw new Error(await readError(res));
}

export async function logout(): Promise<void> {
  const res = await fetch(`${API_BASE_URL}/logout`, {
    method: "POST",
    credentials: "include",
  });

  if (!res.ok) throw new Error(await readError(res));
}

export async function me(): Promise<MeResponse> {
  const res = await fetch(`${API_BASE_URL}/me`, {
    credentials: "include",
  });

  if (!res.ok) return null;

  // Ton backend peut renvoyer un objet user ou juste 204.
  const text = await res.text();
  if (!text) return null;
  return JSON.parse(text) as MeResponse;
}
