import { LoadingIndicator } from "@/components/core/loading-indicator";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { useState } from "react";
import { toast } from "sonner";
import {Link} from "react-router-dom"

function LoginPage() {
  const [username, setUsername] = useState("")
  const [password, setPassword] = useState("")
  const [loading, setLoading] = useState(false)

  async function login() {

    setLoading(true)

    const method = 'POST'

    const body = JSON.stringify({
      username,
      password,
    })

    const headers = {
      'Content-Type': 'application/json'
    }

    const response = await fetch('https://localhost:7055/api/auth/login', {
      method,
      body,
      headers
    })

    const data = await response.text()

    if (response.status != 200) {

      toast.error(data)

    } else {

      localStorage.setItem('token', data)
      window.location.href = '/dashboard'
    }


    setLoading(false)

  }



  return (
    <section className="space-y-2">

      <Input
        value={username}
        onChange={(e) => setUsername(e.target.value)}
        placeholder="username" />
      <Input
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        placeholder="password"
              type="password" />
          <section className=" gap-2 flex-col flex">
      <Button
        onClick={login}>
        {loading && <LoadingIndicator />}
        Login

              </Button>
              <Link to="/" ><Button>
                  Return
              </Button></Link>

          </section>

    </section>
  );
}

export default LoginPage;