import pytest
from .base import AuthorsBaseTest
from django.contrib.auth.models import User
from django.urls import reverse
from selenium.webdriver.common.by import By

@pytest.mark.functional_test
class AuthorsLoginTest(AuthorsBaseTest):
    def test_user_valid_data_can_login_successfully(self):
        string_password = 'pass'
        user = User.objects.create_user(
            username='my_user',
            password=string_password
            )

        #Usuario abre a página de login

        self.browser.get(self.live_server_url + reverse('authors:login'))
        
        #usuario vê o formulário de login
        form = self.browser.find_element(By.CLASS_NAME, 'main-form')
        username_field = self.get_by_placeholder(form,'Type your username')
        password_field = self.get_by_placeholder(form,'Type your password')
        
        #Usuário digita seu usuário e senha
        username_field.send_keys(user.username)
        password_field.send_keys(string_password)

        #Usuário envia o formuluario
        form.submit()

        #Usuário vê a mensagem de login com sucesso e seu nome
        self.assertIn(
            f'Your are logged in with {user.username}',
            self.browser.find_element(By.TAG_NAME,'body').text
        )
        
    def test_login_create_raises_404_if_not_POST_method(self):
        self.browser.get(
            self.live_server_url +
            reverse('authors:login_create')
        )

        self.assertIn(
            'Not Found',
            self.browser.find_element(By.TAG_NAME, 'body').text
        )

    def test_form_login_is_invalid(self):
        #Usuário abre a página de login
        self.browser.get(
            self.live_server_url + reverse('authors:login')
        )

        form = self.fill_login_form(username_value='',password_value='')

        # Envia o formulário
        form.submit()

        # Vê uma mensagem de erro na tela
        self.assertIn(
            'Invalid username or password',
            self.browser.find_element(By.TAG_NAME,'body').text
        )

    def test_form_login_invalid_credentials(self):
        #Usuário abre a página de login
        self.browser.get(
            self.live_server_url + reverse('authors:login')
        )

        form = self.fill_login_form(username_value='invalid_user',password_value='invalid_password')
       
        form.submit()

        # Vê uma mensagem de erro na tela
        self.assertIn(
            'Invalid credentials',
            self.browser.find_element(By.TAG_NAME,'body').text
        )

    
