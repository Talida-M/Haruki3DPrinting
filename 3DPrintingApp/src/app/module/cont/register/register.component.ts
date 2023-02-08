import { Component, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterService } from 'src/app/services/register.service';
import { MyErrorStateMatcher } from '../login/login.component';
import { ErrorStateMatcher } from "@angular/material/core";
type NumberArray = Array<string>;



@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  accountTypes: string[] = ["Client", "Admin", "Executant", "Proiectant" ];
  public registerForm: FormGroup = new FormGroup(
    {
      nume: new FormControl(''),
      prenume: new FormControl(''),
      telefon: new FormControl(''),
      email: new FormControl(''),
      rol: new FormControl("Client") ,
      password: new FormControl(''),
      
    });
    emailFormControl = new FormControl("", [
      Validators.required,
      Validators.email,
    ]);
    passwordFormControl = new FormControl("", [
      Validators.required,
      Validators.pattern("^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{8,}$")
    ]);
  
    matcher = new MyErrorStateMatcher();
  constructor(
    private router: Router,
    private registerService: RegisterService
  ) { }

  //getters
  get email(): AbstractControl {
    return this.registerForm;
  }
  get password(): AbstractControl {
    return this.registerForm;
  }
  get rol(): AbstractControl {
    return this.registerForm;
  }

  ngOnInit(): void {
  }


  // public addAngajati(): void{
  //   this.registerService.addAngajati(this.registerForm.value).subscribe(
  //     (result) => {
  //       console.log(result);
  //     },
  //     (error) => {
  //       console.log(error);
  //     }
  //   );

  //   console.log(this.registerForm.value);
  //   this.router.navigate(['/login'])
  // }

  public register(): void{
    this.registerService.addRegister(this.registerForm.value).subscribe(
      (result) => {
        console.log(result);
      },
      (error) => {
        console.log(error);
      }
     
    );
    var r = this.rol

    console.log(this.registerForm.value);
    this.router.navigate(['/login'])
  }

 

}
