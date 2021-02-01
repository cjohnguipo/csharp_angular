import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';
import { delay, map, switchMap } from 'rxjs/operators';
import { of, timer } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  private passwordRegEx: any = environment.passwordRegEx;
  private emailRegEx: any = environment.emailRegEx;
  registerForm: FormGroup;
  errors: string[];

  constructor(private fb: FormBuilder,
              private accountService: AccountService,
              private router: Router) { }

  ngOnInit(): void {
    this.createRegistrationForm();
  }

  createRegistrationForm() {
    this.registerForm = this.fb.group({
      displayName: [null,
                    [Validators.required]
                   ],
      email: [null,
              [Validators.required, Validators.pattern(this.emailRegEx)],
              [this.validateEmailNotTaken()]
             ],
      password: [null,
                 [Validators.required, Validators.pattern(this.passwordRegEx)]],
    });
  }

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/shop');
    }, error => {
      console.log(error);
      this.errors = error.errors;
    });
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.accountService.checkEmailExists(control.value).pipe(
            map(res => {
              return res ? {emailExists: true} : null;
            })
          );
        })
      );
    };
  }

}
