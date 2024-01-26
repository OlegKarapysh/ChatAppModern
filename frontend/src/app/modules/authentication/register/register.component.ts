import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { RegisterDto } from '../../../models/register-dto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from '../../../validations/custom-validators';

@Component({
    selector: 'chat-register',
    templateUrl: './register.component.html',
    styleUrl: './register.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterComponent implements OnInit {
    public registerDto: RegisterDto | undefined;

    public registerForm: FormGroup = new FormGroup({});

    public isPasswordHidden = true;

    public isConfirmPasswordHidden = true;

    constructor(private readonly formBuilder: FormBuilder) {}

    public ngOnInit(): void {
        this.registerForm = this.formBuilder.group({
            username: [
                '',
                [
                    Validators.required,
                    Validators.minLength(3),
                    Validators.maxLength(100),
                ],
            ],
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
            confirmPassword: [
                '',
                [Validators.required, CustomValidators.checkMatch('password')],
            ],
        });
    }

    public register(): void {
        this.registerDto = {
            username: this.registerForm.value.username,
            email: this.registerForm.value.email,
            password: this.registerForm.value.password,
            confirmPassword: this.registerForm.value.confirmPassword,
        };

        console.log(this.registerDto);
    }
}
