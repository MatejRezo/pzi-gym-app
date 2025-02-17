export enum ENVIRONMETS {
  local = 'http://localhost:8080/api',
  production = 'https://sportiverse-dotnet-api.herokuapp.com/api'
};

export const getCurrentEnv = () => ENVIRONMETS.local;
