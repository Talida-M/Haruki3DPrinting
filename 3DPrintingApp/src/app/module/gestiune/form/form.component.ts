import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ContClientService } from 'src/app/services/cont-client.service';
import { DataService } from 'src/app/services/data.service';
//import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {

  public loginForm: FormGroup = new FormGroup(
    {
      password: new FormControl('')
    });


  constructor(
    private router: Router,
    private dataService: DataService,
    private contService: ContClientService
    //private loginService: LoginService
    ) { }

    //getters
    
    get password(): AbstractControl {
      return this.loginForm;
    }

  ngOnInit(): void {

  }



  public login(): void {
    var parola = this.loginForm.controls['password'].value;
    var verify = this.contService.getPasswordAngajat(parola);
    if (verify)
       {
        this.router.navigate(['/gestiune']);
       //localStorage.setItem('Parola', parola);
       
      }
      else{
        console.log(parola);
        alert("Parola Incorecta. Daca nu sunteti angajat sau admin nu puteti intra in pagina de Gestiune!!")
        localStorage.setItem('Parola', '');
      }
      (error) => {
        console.log(error);
      }
   

    this.dataService.changeUserData(this.loginForm.value);
   //this.router.navigate(['/gestiune'])

  }
}
