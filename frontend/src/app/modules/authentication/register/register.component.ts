import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { RegisterDto } from '../../../models/auth/register-dto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from '../../../validations/custom-validators';
import { UnsubscribingComponent } from '../../../shared/components/unsubscribing-component';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorDetails } from '../../../models/errors/error-details';
import { ToastrService } from 'ngx-toastr';
import { LoadingService } from '../../../services/loading.service';
import { SpinnerService } from '../../../services/spinner.service';

@Component({
    selector: 'chat-register',
    templateUrl: './register.component.html',
    styleUrl: './register.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterComponent
    extends UnsubscribingComponent
    implements OnInit
{
    public registerDto: RegisterDto | undefined;
    public registerForm: FormGroup = new FormGroup({});
    public isPasswordHidden = true;
    public isConfirmPasswordHidden = true;

    constructor(
        public readonly loadingService: LoadingService,
        private readonly formBuilder: FormBuilder,
        private readonly authService: AuthService,
        private readonly router: Router,
        private readonly toastService: ToastrService,
        private readonly spinner: SpinnerService
    ) {
        super();
    }

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
        this.loadingService.startLoading();
        this.spinner.show();
        this.registerDto = {
            username: this.registerForm.value.username,
            email: this.registerForm.value.email,
            password: this.registerForm.value.password,
            confirmPassword: this.registerForm.value.confirmPassword,
        };

        this.authService
            .register(this.registerDto)
            .pipe(takeUntilDestroyed(this.destroyRef))
            .subscribe({
                next: () => {
                    this.finishLoading();
                    this.toastService.clear();
                    this.router.navigateByUrl('/');
                },
                error: (err: HttpErrorResponse) => {
                    this.finishLoading();
                    this.toastService.error(
                        (err.error as ErrorDetails)?.detail ?? err.error.title
                    );
                },
            });
    }

    private finishLoading(): void {
        this.loadingService.finishLoading();
        this.spinner.hide();
    }
}
