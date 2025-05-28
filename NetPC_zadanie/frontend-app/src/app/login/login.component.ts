import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) { }

  onLogin() {
    this.authService.login(this.username, this.password).subscribe({
      next: (response: { token: string, message: string }) => {
        localStorage.setItem('authToken', response.token);
        localStorage.setItem('currentUser', this.username);
        this.router.navigate(['/contacts']);
      },
      error: (err) => {
        this.errorMessage = 'Nieprawidłowa nazwa użytkownika lub hasło!';
      }
    });
  }

  goToHome() {
    this.router.navigate(['/']);
  }
}
