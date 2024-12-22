import { useState } from "react";
import { Input } from "../ui/input";
import { Button } from "../ui/button";
import { Plus } from "lucide-react";
import { Select, SelectContent, SelectGroup, SelectItem, SelectLabel, SelectTrigger, SelectValue } from "../ui/select";
import { LoadingIndicator } from "./loading-indicator";
import { Action, User } from "@/pages/dashboard/ActionsDatatble";
import { toast } from "sonner";


export function AddActionForm() {

    const [devicetype, setDevicetype] = useState('')

    const [region, setRegion] = useState('')

    const [application, setApplication] = useState('')

    const [loading, setLoading] = useState(false)
    const [timedate, setTimedate] = useState(new Date())


    async function addAction() {

        if (loading) {
            return
        }

        setLoading(true)

        const user: User = {
            Id: "",
            Name: "",
            Username: ""
        }

        const action: Action = {
            id: "",
            userId: "",
            devicetype,
            region,
            application,
            timedate,
            user,
        }

        const token = localStorage.getItem('token')
        const method = 'POST'
        const url = `https://localhost:7055/api/actions/add?token=${token}`
        const headers = {
            'Content-Type': 'application/json'
        }

        // Remove the `form` wrapper, sending the action object directly
        const body = JSON.stringify(action)

        const response = await fetch(url, { method, headers, body })
        const data = await response.text()

        if (response.status == 200) {
            window.location.reload()
        } else {
            toast.error(data)
        }

        setLoading(false)
    }


    return (


        <section className="grid grid-cols-5 w-full gap-2 py-2">
            <Select onValueChange={(setDevicetype)}>
                <SelectTrigger>
                    <SelectValue placeholder="Zgjidh pajisjen" />
                </SelectTrigger>
                <SelectContent>
                    <SelectGroup>
                        <SelectLabel className="opacity-50">Pajisjet</SelectLabel>
                        <SelectItem value="desktop">Desktop</SelectItem>
                        <SelectItem value="web">Web</SelectItem>
                        <SelectItem value="mobile">Mobile</SelectItem>
                    </SelectGroup>
                </SelectContent>
            </Select>
            <Select onValueChange={setRegion}>
                <SelectTrigger>
                    <SelectValue placeholder="Zgjidh Rajonin" />
                </SelectTrigger>
                <SelectContent>
                    <SelectGroup>
                        <SelectLabel className="opacity-50">Rajoni</SelectLabel>
                        <SelectItem value="Asia">Asia</SelectItem>
                        <SelectItem value="Europe">Europe</SelectItem>
                        <SelectItem value="America">America</SelectItem>
                    </SelectGroup>
                </SelectContent>
            </Select>
            <Input
                placeholder="Aplikacioni"
                value={application}
                onChange={(e) => setApplication(e.target.value)} />
            <Input
                type="date"
                defaultValue={new Date().toISOString().split('T')[0]}
                onChange={(e) => setTimedate(new Date(e.target.value))} />
            <Button onClick={addAction}>
                {loading ?
                    <LoadingIndicator /> :
                    <Plus />
                }
                Ruaj
            </Button>
        </section>

    )


}