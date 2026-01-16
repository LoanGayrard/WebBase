const API_BASE_URL = import.meta.env.VITE_API_BASE_URL + "/auth";

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
  isEmailConfirmed: boolean;
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
  const res = await fetch(`${API_BASE_URL}/login?useCookies=true&useSessionCookies=false`, {
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
  const res = await fetch(`${API_BASE_URL}/manage/info`, {
    credentials: "include",
  });

  if (!res.ok) return null;

  const text = await res.text();
  if (!text) return null;
  return JSON.parse(text) as MeResponse;
}
