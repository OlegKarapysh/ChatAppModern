import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { LoginDto } from '../../../models/auth/login-dto';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from '../../../validations/custom-validators';
import { UnsubscribingComponent } from '../../../shared/components/unsubscribing-component';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ErrorDetails } from '../../../models/errors/error-details';
import { HttpErrorResponse } from '@angular/common/http';
import { SpinnerService } from '../../../services/spinner.service';
import { BehaviorSubject } from 'rxjs';
import { LoadingService } from '../../../services/loading.service';

@Component({
    selector: 'chat-login',
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent extends UnsubscribingComponent implements OnInit {
    public loginDto: LoginDto | undefined;
    public loginForm: FormGroup = new FormGroup({});
    public isPasswordHidden = true;

    constructor(
        public readonly loadingService: LoadingService,
        private readonly formBuilder: FormBuilder,
        private readonly router: Router,
        private readonly toastService: ToastrService,
        private readonly authService: AuthService,
        private readonly spinner: SpinnerService
    ) {
        super();
    }

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
        this.loadingService.startLoading();
        this.spinner.show();
        this.loginDto = {
            email: this.loginForm.value.email,
            password: this.loginForm.value.password,
        };

        this.authService
            .login(this.loginDto)
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
