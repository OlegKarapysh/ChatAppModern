import { JwtPayload } from 'jwt-decode';

export type JwtAuthPayload = JwtPayload & {
    id: string;
    username: string;
    email: string;
};
