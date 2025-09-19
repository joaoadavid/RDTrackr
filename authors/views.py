# authors/views.py
from django.contrib import messages
from django.contrib.auth import authenticate, login, logout
from django.contrib.auth.mixins import LoginRequiredMixin
from django.shortcuts import redirect
from django.urls import reverse, reverse_lazy, NoReverseMatch
from django.views import View
from django.views.generic import FormView
from authors.forms import LoginForm, RegisterForm

class RegisterView(FormView):
    template_name = "authors/register.html"
    form_class = RegisterForm
    success_url = reverse_lazy("authors:login")

    def get_initial(self):
        initial = super().get_initial()
        data = self.request.session.get("register_form_data", {})
        if isinstance(data, dict):
            initial.update(data)
        return initial

    def form_valid(self, form):
        user = form.save(commit=False)
        raw_pwd = form.cleaned_data.get("password")
        if raw_pwd:
            user.set_password(raw_pwd)
        user.save()
        messages.success(self.request, "Your user is created, please log in.")
        self.request.session.pop("register_form_data", None)
        return super().form_valid(form)

    def form_invalid(self, form):
        post = {k: v for k, v in self.request.POST.items()
                if k not in ("password", "password2", "csrfmiddlewaretoken")}
        self.request.session["register_form_data"] = post
        messages.error(self.request, "There are errors in the form, please fix them and send again.")
        return super().form_invalid(form)

class AuthLoginView(FormView):
    template_name = "authors/login.html"
    form_class = LoginForm

    def form_valid(self, form):
        user = authenticate(self.request,
                            username=form.cleaned_data.get("username",""),
                            password=form.cleaned_data.get("password",""))
        if user is None:
            messages.error(self.request, "Invalid credentials")
            return super().form_invalid(form)

        login(self.request, user)
        messages.success(self.request, "Your are logged in.")
        if self.request.POST.get("remember"):
            self.request.session.set_expiry(60*60*24*30)
        else:
            self.request.session.set_expiry(0)

        next_url = self.request.POST.get("next") or self.request.GET.get("next")
        if not next_url:
            try:
                next_url = reverse("authors:dashboard")
            except NoReverseMatch:
                next_url = "/"
        return redirect(next_url)

    def form_invalid(self, form):
        messages.error(self.request, "Invalid username or password")
        return super().form_invalid(form)

class LogoutStrictView(LoginRequiredMixin, View):
    login_url = reverse_lazy("authors:login")
    def post(self, request):
        if request.POST.get("username") != request.user.get_username():
            messages.error(request, "Invalid logout user")
            return redirect(reverse("authors:login"))
        logout(request)
        messages.success(request, "Logged out successfully")
        return redirect(reverse("authors:login"))
    def get(self, request):
        messages.error(request, "Invalid logout request")
        return redirect(reverse("authors:login"))
