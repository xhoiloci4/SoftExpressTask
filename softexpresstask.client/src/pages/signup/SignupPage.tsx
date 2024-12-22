import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { useState } from "react";
import { toast } from "sonner";
import {Link } from "react-router-dom"

export function SignupPage() {
    const [name, setName] = useState('')
    const [email, setEmail] = useState('')
    const [username, setUsername] = useState('')
    const [password, setPassword] = useState('')
    const [birthdate, setBirthdate] = useState(new Date())

    async function signup() {



        const url = 'https://localhost:7055/api/auth/signup'

        const method = 'POST'

        const headers = {
            'Content-Type': 'application/json'
        }

        const body = JSON.stringify({
            name,
            username,
            email,
            password,
            birthdate,
        })

        const response = await fetch(url, {
            method,
            headers,
            body
        })


        const data = await response.text()


        if (response.status == 200) {

            localStorage.setItem('token', data)
            window.location.href = '/dashboard'
        } else {
            toast.error(data)
        }
    }


    return (
        <section className='space-y-2'>

            <Input
                placeholder="Name"
                value={name}
                onChange={(e) => setName(e.target.value)} />
            <Input
                placeholder="Username"
                value={username}
                onChange={(e) => setUsername(e.target.value)} />
            <Input
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)} />
            <Input
                placeholder="Password"
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)} />

            <Input
                placeholder="Birthdate"
                type="date"
                onChange={(e) => setBirthdate(new Date(e.target.value))} />
            <section className="flex flex-col gap-2">
            <Button onClick={signup}>
                Sign up
            </Button>
                <Link to="/" ><Button > Return </Button></Link>

            </section>

        </section>

    )


}