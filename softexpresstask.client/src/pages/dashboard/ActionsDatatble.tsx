import { AddActionForm } from "@/components/core/add-action-form"
import { LoadingIndicator } from "@/components/core/loading-indicator"
import { Input } from "@/components/ui/input"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { ColumnDef, flexRender, getCoreRowModel, getFilteredRowModel, useReactTable } from "@tanstack/react-table"
import { useEffect, useState } from "react"
import { toast } from "sonner"

export interface User {
    Id: string
    Name: string
    Username: string
}

export interface Action {
    id: string
    userId: string
    devicetype: string
    region: string
    application: string
    timedate: Date
    user: User
}


const columns: ColumnDef<Action>[] = [

    {
        accessorKey: "devicetype",
        header: "Pajisja",
    },
    {
        accessorKey: "application",
        header: "Aplikacioni",
    },
    {
        accessorKey: "timedate",
        header: "Data",
    },

]


export function ActionsDatatable() {
    const [loading, setLoading] = useState(false)
    const [actions, setActions] = useState<Action[]>([])

    const table = useReactTable({
        data: actions,
        columns,
        getCoreRowModel: getCoreRowModel(),
        getFilteredRowModel: getFilteredRowModel(),
    })


    async function getActions() {

        setLoading(true)

        const token = localStorage.getItem('token')
        const method = 'GET'
        const url = `https://localhost:7055/api/actions/getAll?token=${token}`
        const headers = {
            'Content-Type': 'application/json'
        }
        const response = await fetch(url, { method, headers })
        const data = await response.json()
        setTimeout(() => {
            if (response.status == 200) {

                setActions(data.actions)


            } else {
                toast.error(data)

            }
            setLoading(false)
        }, 500);
    }


    useEffect(() => {

        getActions()

    }, [])


    return (

        <section className="w-full">
            <div className="flex">
                <Input
                    placeholder="Search"
                    onChange={(e) => table.setGlobalFilter(e.target.value)} />
                {loading && <LoadingIndicator />}

            </div>
            <AddActionForm />
            <div className="rounded-md border w-full">

                <Table className="w-full">
                    <TableHeader>
                        {table.getHeaderGroups().map((headerGroup) => (
                            <TableRow key={headerGroup.id}>
                                {headerGroup.headers.map((header) => {
                                    return (
                                        <TableHead key={header.id}>
                                            {header.isPlaceholder
                                                ? null
                                                : flexRender(
                                                    header.column.columnDef.header,
                                                    header.getContext()
                                                )}
                                        </TableHead>
                                    )
                                })}
                            </TableRow>
                        ))}
                    </TableHeader>
                    <TableBody>
                        {
                            table.getRowModel().rows?.length ? (
                                table.getRowModel().rows.map((row) => (
                                    <TableRow
                                        key={row.id}
                                        data-state={row.getIsSelected() && "selected"}
                                    >
                                        {row.getVisibleCells().map((cell) => (
                                            <TableCell key={cell.id}>
                                                {flexRender(cell.column.columnDef.cell, cell.getContext())}
                                            </TableCell>
                                        ))}
                                    </TableRow>
                                ))
                            ) : (
                                <TableRow>
                                    <TableCell colSpan={columns.length} className="h-24 text-center">
                                        No results.
                                    </TableCell>
                                </TableRow>
                            )}
                    </TableBody>
                </Table>
            </div>
        </section>
    )


}
