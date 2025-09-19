from django import forms

class ContactForm(forms.Form):
    name = forms.CharField(label="Seu nome", max_length=120,
                           widget=forms.TextInput(attrs={"class":"input input-bordered w-full", "placeholder":"Seu nome"}))
    email = forms.EmailField(label="Seu e-mail",
                           widget=forms.EmailInput(attrs={"class":"input input-bordered w-full", "placeholder":"voce@exemplo.com"}))
    subject = forms.CharField(label="Assunto", max_length=180, required=False,
                           widget=forms.TextInput(attrs={"class":"input input-bordered w-full", "placeholder":"Assunto"}))
    message = forms.CharField(label="Mensagem",
                           widget=forms.Textarea(attrs={"class":"textarea textarea-bordered w-full", "rows":5, "placeholder":"Como podemos ajudar?"}))
