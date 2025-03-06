import {Container} from "semantic-ui-react";
import {observer} from "mobx-react-lite";
import {Outlet, useLocation} from "react-router-dom";
import HomePage from "../../features/home/HomePage.tsx";
import Navbar from "./Navbar.tsx";

function AppLayout() {
    const location = useLocation();

    return (
        <>
            {location.pathname === "/" ? <HomePage /> : (
                <>
                    <Navbar />
                    <Container style={{marginTop: "7em"}}>
                        <Outlet />
                    </Container>
                </>
            )}
        </>
    )
}

export default observer(AppLayout);