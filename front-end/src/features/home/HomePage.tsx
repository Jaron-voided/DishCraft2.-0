import { Button, Container, Header, Segment, Image } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { useStore } from "../../app/stores/store.ts";
import LoginForm from "../users/LoginForm.tsx";
import RegisterForm from "../users/RegisterForm.tsx";
import styles from "./HomePage.module.css";
import Navbar from "../../app/layout/Navbar.tsx";

export default observer(function HomePage() {
    const { userStore, modalStore } = useStore();

    return (
        <Segment
            inverted
            textAlign="center"
            vertical
            className={styles.masthead}
        >
            <Container text>
                {/* Logo Image */}
                <Image
                    className={styles.headerImage}
                    src="/assets/bigLogo.jpg"
                    alt="logo"
                    centered
                    size="big"
                />

                {/* Conditional Rendering for User State */}
                {userStore.isLoggedIn ? (
                    <>
                        {/* NavBar and Personalized Welcome */}
                        <Navbar />
                        <Header
                            as="h1"
                            className={styles.welcomeHeader}
                            inverted
                        >
                            Welcome back, {userStore.user?.displayName}!
                        </Header>
                    </>
                ) : (
                    <>
                        {/* Welcome Header */}
                        <Header
                            as="h1"
                            className={styles.welcomeHeader}
                            inverted
                        >
                            Welcome to DishCraft
                        </Header>

                        {/* Login/Register Buttons */}
                        <div className={styles.buttonContainer}>
                            <Button
                                onClick={() => modalStore.openModal(<LoginForm />)}
                                size="huge"
                                inverted
                            >
                                Login!
                            </Button>
                            <Button
                                onClick={() => modalStore.openModal(<RegisterForm />)}
                                size="huge"
                                inverted
                            >
                                Register!
                            </Button>
                        </div>
                    </>
                )}
            </Container>
        </Segment>
    );
});