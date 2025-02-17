export type LoginDetails = {
  username: string,
  password: string,
}

export type LocalStorageToken = {
  token: string,
  refreshToken: string,
  created?: string,
  expires?: string,
}