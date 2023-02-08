import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    const isAuthorized = localStorage.getItem('Role') === 'Client';
    if (isAuthorized ||  localStorage.getItem('Rol') === 'Admin'){
      const div_cont: HTMLElement = document.getElementById('contul-meu');
      div_cont.style.display = 'block';
      const div_cos: HTMLElement = document.getElementById('cosul-meu');
      div_cos.style.display = 'block';
    

    }
    if ( localStorage.getItem('Rool') === 'Executant' ||  localStorage.getItem('Rool') === 'Proiectant' ||  localStorage.getItem('Rool') === 'Admin'){
      const div_ang: HTMLElement = document.getElementById('pagina-angajati');
      div_ang.style.display = 'block';
      const register: HTMLElement = document.getElementById('register-meu');
      register.style.display = 'none';
      const login: HTMLElement = document.getElementById('cont-login');
      login.style.display = 'none';
      const div_cont: HTMLElement = document.getElementById('contul-meu');
      div_cont.style.display = 'block';

    }
    if (localStorage.getItem('Role') === 'Anonim') {
      const register: HTMLElement = document.getElementById('register-meu');
      register.style.display = 'block';
      const login: HTMLElement = document.getElementById('cont-login');
      login.style.display = 'block';
    }
  }

}
