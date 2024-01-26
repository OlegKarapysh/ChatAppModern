import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { LoginDto } from '../../../models/login-dto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from '../../../validations/custom-validators';

@Component({
    selector: 'chat-login',
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent implements OnInit {
    public loginDto: LoginDto | undefined;

    public loginForm: FormGroup = new FormGroup({});

    public isPasswordHidden = true;

    constructor(private readonly formBuilder: FormBuilder) {}

    public ngOnInit(): void {
        this.loginForm = this.formBuilder.group({
            email: [
                '',
                [
                    Validators.required,
                    Validators.email,
                    Validators.minLength(3),
                    Validators.maxLength(100),
                ],
            ],
            password: [
                '',
                [
                    Validators.required,
                    Validators.minLength(6),
                    Validators.maxLength(16),
                    CustomValidators.checkPasswordStrong(),
                ],
            ],
        });
    }

    public login(): void {
        this.loginDto = {
            email: this.loginForm.value.email,
            password: this.loginForm.value.password,
        };

        console.log(this.loginDto);
    }
}
