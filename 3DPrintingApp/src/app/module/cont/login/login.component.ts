import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router, RouteReuseStrategy } from '@angular/router';
import { DataService } from 'src/app/services/data.service';
import { LoginService } from 'src/app/services/login.service';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(
    control: FormControl | null,
    form: FormGroupDirective | NgForm | null
  ): boolean {
    const isSubmitted = form && form.submitted;
    return !!(
      control &&
      control.invalid &&
      (control.dirty || control.touched || isSubmitted)
    );
  }
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})


export class LoginComponent implements OnInit {



  public loginForm: FormGroup = new FormGroup(
    {
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required)
    });


    matcher = new MyErrorStateMatcher();
  constructor(
    private router: Router,
    private dataService: DataService,
    private loginService: LoginService
    ) { }

    //getters
    get email(): AbstractControl {
      return this.loginForm;
    }
    get password(): AbstractControl {
      return this.loginForm;
    }

  ngOnInit(): void {

  }

  public logout(): void{
    localStorage.setItem('Role','Anonim');
    localStorage.setItem("Cumparaturi", ""); 

  }
  

  public login(): void {
    console.log(this.loginForm.controls['email'].value);
    var info = this.loginForm.controls['email'].value;
    this.loginService.addLogin(this.loginForm.value).subscribe(
      (result) => {
        console.log(result);      
        this.dataService.changeUserData(this.loginForm.value);  
       localStorage.setItem('Role', 'Client');
        this.loginService.getRole(info).subscribe(
          (result)=>{
            localStorage.setItem('Rool', result['rol']);
          } );
        var user = this.loginForm.controls['email'].value;
        localStorage.setItem('User',user);
        this.router.navigate(['/cont'])
        setTimeout(function(){
          window.location.reload();
       }, 5000);
       // window.location.reload();
      },
      (error) => {
        console.log(error);
      }
    );

   
    

  }




}