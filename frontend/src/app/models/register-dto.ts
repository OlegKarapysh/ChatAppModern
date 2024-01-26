import { LoginDto } from './login-dto';

export type RegisterDto = LoginDto & {
    username: string;
    confirmPassword: string;
};
