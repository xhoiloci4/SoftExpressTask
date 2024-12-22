import { Link } from "react-router-dom";
import { ActionsDatatable } from "./ActionsDatatble";


export function DashboardPage() {


    return (
        <div className="flex flex-col gap-2">
        <ActionsDatatable />
        <Link to="/reports" className="w-1/4">Go to Reports</Link>
        </div>

    )



}