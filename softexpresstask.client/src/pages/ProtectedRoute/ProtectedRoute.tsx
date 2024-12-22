import React from 'react';
import { Navigate, Outlet, Link } from 'react-router-dom';

import { Card, CardContent, CardFooter, CardHeader } from '../../components/ui/card';
import { Button } from '../../components/ui/button';


const ProtectedRoute = () => {

    if (!localStorage.getItem("token")){
        return (
            <Card>
                <CardHeader>You Are not Authorised</CardHeader>
                <CardContent>We are sorry.
                    The requested resource is for Authorised Users Only.</CardContent>
                <CardFooter className="flex gap-4 justify-around">
                    <div>
                    <div>You Already have an account ?</div>
                        <Link to="/login"><Button>Log In</Button></Link>
                    </div>
                    <div>
                    <div>You want to register ?</div>
                        <Link to="/signup"><Button>Sign Up</Button></Link>
                    </div>
                </CardFooter>
            </Card>


        )
    }

    return <Outlet />;
};

export default ProtectedRoute;