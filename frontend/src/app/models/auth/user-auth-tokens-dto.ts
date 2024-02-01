import { AuthTokensDto } from './auth-tokens-dto';

export type UserAuthTokensDto = AuthTokensDto & {
    userId: string;
};
