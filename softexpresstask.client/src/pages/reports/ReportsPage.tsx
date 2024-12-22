import { useState, useEffect } from "react";
import { LineChartComp } from "@/components/core/line-chart";
import { Select, SelectContent, SelectGroup, SelectItem, SelectLabel, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Search } from "lucide-react";
import { LoadingIndicator } from "@/components/core/loading-indicator";
import { Action } from "../dashboard/ActionsDatatble";

export function ReportsPage() {
    const [chartData, setChartData] = useState<Action[]>([]);
    const [loading, setLoading] = useState(false);
    const [devicetype, setDevicetype] = useState('')
    const [region, setRegion] = useState('')
    const [application, setApplication] = useState('')
    const [timedate, setTimedate] = useState(new Date())


    async function fetchChartData() {

        setLoading(true)
        const data = timedate.toISOString().split("T")[0];

        const url = `https://localhost:7055/api/reports/GetAll?rajoni=${region}&lloji=${devicetype}&aplikacioni=${application}&data=${data}`;

        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        const result = await response.json();
        const actions = result.actions;

        setChartData(actions)
        setLoading(false)

    }

    useEffect(() => {
        fetchChartData();
    }, []);

    return (
        <section>
            <div className="grid grid-cols-5 w-full">
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
                <Button onClick={fetchChartData}>
                    {loading ?
                        <LoadingIndicator /> :
                        <Search />
                    }
                    Filtro
                </Button>
            </div>
            {loading ? (
                <div>Loading...</div>
            ) : (
                <div className="grid grid-cols-2 gap-2 py-2">
                    <LineChartComp data={chartData} />
                    <LineChartComp data={chartData} />
                </div>
            )}
        </section>
    );
}
