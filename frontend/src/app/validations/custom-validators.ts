import { AbstractControl, ValidatorFn } from '@angular/forms';

export class CustomValidators {
    public static checkPasswordStrong(): ValidatorFn {
        return (control: AbstractControl) =>
            /(^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[_\W]).+$)/.test(
                control.value
            )
                ? null
                : { weakPassword: true };
    }

    public static checkMatch(matchingControlName: string): ValidatorFn {
        return (control: AbstractControl) =>
            control.value === control.parent?.get(matchingControlName)?.value
                ? null
                : { notMatching: true };
    }
}
