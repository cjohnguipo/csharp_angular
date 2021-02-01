import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  private passwordRegEx: any = environment.passwordRegEx;
  private emailRegEx: any = environment.emailRegEx;
  loginForm: FormGroup;
  returnUrl: string;

  constructor(private accountService: AccountService,
              private router: Router,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '/shop';
    this.createLoginForm();
  }

  createLoginForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required,
                                  Validators.pattern(this.emailRegEx)]
                            ),
      password: new FormControl('', [Validators.required,
                                     Validators.pattern(this.passwordRegEx)])
    });
  }

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe(() => {
      // console.log('User Logged In');
      this.router.navigateByUrl(this.returnUrl);
    }, error => {
      console.log(error);
    });
  }

}
