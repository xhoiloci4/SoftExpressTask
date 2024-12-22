import { cn } from "@/lib/utils";
import { Loader2 } from "lucide-react";

interface Props {
    className?: string
}


export function LoadingIndicator(props: Props) {

    return (
        <Loader2
            className={cn("animate-spin", props.className)}
        />
    )



}