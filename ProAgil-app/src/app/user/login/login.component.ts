import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  titulo = 'Login';
  model: any = {};

  constructor(
    public router: Router,
    private toastr: ToastrService,
    private auth: AuthService
  ) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null) {
      this.router.navigate(['/dashboard']);
    }
  }

  login() {
    this.auth.login(this.model)
      .subscribe(
        () => {
          this.router.navigate(['/dashboard']);
          // this.toastr.info(`Olá ${this.model.username}!`);
        },
        error => {
          const erro = error.error;
          this.toastr.error(
            `Falha ao tentar Logar.\n
             Código do erro: ${erro.code}`
          );
        }
      );
  }

}
