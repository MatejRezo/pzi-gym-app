export enum ENVIRONMETS {
  local = 'http://localhost:8080/api',
  production = 'https://pzi-gym.sumit.ba/api'
};

export const getCurrentEnv = () => ENVIRONMETS.production;
