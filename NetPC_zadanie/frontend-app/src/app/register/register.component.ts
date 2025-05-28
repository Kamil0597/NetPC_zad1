import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  username = '';
  password = '';
  errorMessage = '';
  successMessage = '';

  constructor(private authService: AuthService, private router: Router) { }

  onRegister() {
    if (!this.username || !this.password) {
      this.errorMessage = 'Wszystkie pola są wymagane!';
      return;
    }

    // Walidacja złożoności hasła
    const passwordComplexityRegex = /^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]).{8,}$/;
    if (!passwordComplexityRegex.test(this.password)) {
      this.errorMessage = 'Hasło musi mieć co najmniej 8 znaków, 1 wielką literę, 1 cyfrę i 1 znak specjalny.';
      this.successMessage = '';
      return;
    }

    
    this.authService.register(this.username, this.password).subscribe({
      next: (res) => {
        this.successMessage = 'Rejestracja udana! Możesz się teraz zalogować.';
        this.errorMessage = '';
        setTimeout(() => this.router.navigate(['/login']), 2000);
      },
      error: (err) => {
        this.errorMessage = 'Rejestracja nieudana! Sprawdź dane i spróbuj ponownie.';
        this.successMessage = '';
      }
    });
  }


  goToHome() {
    this.router.navigate(['/']);
  }
}
