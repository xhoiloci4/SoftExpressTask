import './App.css';
import { BrowserRouter, Route, Routes, Link} from 'react-router-dom'
import LoginPage from './pages/login/LoginPage';
import { Toaster } from './components/ui/sonner';
import { SignupPage } from './pages/signup/SignupPage';
import { DashboardPage } from './pages/dashboard/DashboardPage';
import { ReportsPage } from './pages/reports/ReportsPage';
import { Button } from "@/components/ui/button";
import ProtectedRoute from './pages/ProtectedRoute/ProtectedRoute';


function App() {

    

    return (
        <BrowserRouter>
            <div className='app'>
                <Routes>
                    <Route path="/" Component={SimpleMainPage} />
                    <Route path="/login" Component={LoginPage} />
                    <Route path="/signup" Component={SignupPage} />
                    <Route Component={ProtectedRoute}>
                        <Route path="/dashboard" Component={DashboardPage}/>
                        <Route path="/reports" Component={ReportsPage} />
                    </Route>
                    <Route path="*" Component={ ()=> (<p>Nothing Here</p>)} />
                </Routes>
            </div>
            <Toaster />
        </BrowserRouter>
    );


}


export const SimpleMainPage = () => {
    if (!localStorage.getItem("token")){

        return (
            <section className="flex gap-2">
                <Link to="/login" ><Button>Log in</Button></Link>
                <Link to="/signup"><Button>Sign up</Button></Link>
            </section>
        )
    } else {
        return (<DashboardPage />)
    }
}

export default App;