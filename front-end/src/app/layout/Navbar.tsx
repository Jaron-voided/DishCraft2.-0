import { Dropdown, Image, Menu } from "semantic-ui-react";
import { Link, NavLink } from "react-router-dom";
import { observer } from "mobx-react-lite";
import styles from "./Navbar.module.css";
import {useStore} from "../stores/store.ts";

export default observer(function NavBar() {
    const {
        userStore: { user, logout }
    } = useStore();


    const navItems = [
        {
            name: 'Ingredients',
            color: 'teal',
            className: styles.ingredientsDropdown,
            items: [
                { name: 'View Ingredients', path: '/ingredients', icon: 'eye' },
                { name: 'Add Ingredient', path: '/createIngredient', icon: 'plus' }
            ]
        },
        {
            name: 'Recipes',
            color: 'google plus',
            className: styles.recipesDropdown,
            items: [
                { name: 'View Recipes', path: '/recipes', icon: 'eye' },
                { name: 'Add Recipe', path: '/createRecipe', icon: 'plus' }
            ]
        },
        {
            name: 'DayPlans',
            color: 'pink',
            className: styles.dayPlansDropdown,
            items: [
                { name: 'View DayPlans', path: '/dayPlan', icon: 'eye' },
                { name: 'Add DayPlan', path: '/createDayPlan', icon: 'plus' }
            ]
        }
    ];

    return (
        <Menu inverted fixed="top" className={styles.navBar}>
            <div className={styles.container}>
                {/* Left-aligned logo and text */}
                <Menu.Item as={NavLink} to="/" header className={styles.logoSection}>
                    <Image src={"/assets/logo.png"} alt="logo" className={styles.logo} />
                    <span className={styles.logoText}>Dish Craft</span>
                </Menu.Item>

                {/* Centered dropdown menus */}
                <div className={styles.navButtonsContainer}>
                    {navItems.map((item) => (
                        <Dropdown
                            key={item.name}
                            text={item.name}
                            className={`${styles.navDropdown} ${item.className} ui button ${item.color}`}
                        >
       {/*                     <Dropdown.Menu>
                                {item.items.map((subItem) => (
                                    <Dropdown.Item
                                        key={subItem.name}
                                        as={NavLink}
                                        to={subItem.path}
                                        onClick={() => {
                                            if (subItem.name === 'Add Recipe') {
                                                recipeStore.clearSelectedRecipe();
                                            }
                                        }}
                                        icon={subItem.icon}
                                        text={subItem.name}
                                    />
                                ))}
                            </Dropdown.Menu>*/}
                        </Dropdown>
                    ))}
                </div>

                {/* User dropdown menu */}
                <Menu.Item position="right" className={styles.userSection}>
                    <Dropdown
                        pointing="top right"
                        className={styles.userDropdown}
                        trigger={
                            <span>
                                <Image src={user?.image || "/assets/logo.png"} avatar className={styles.avatar} />
                                {user?.displayName}
                            </span>
                        }
                    >
                        <Dropdown.Menu>
                            <Dropdown.Item as={Link} to={`/profile/${user?.username}`} text="My Profile" icon="user" />
                            <Dropdown.Item onClick={logout} text="Logout" icon="power" />
                        </Dropdown.Menu>
                    </Dropdown>
                </Menu.Item>
            </div>
        </Menu>
    );
});

/*
import { Box, AppBar, Toolbar, Typography, Container, MenuItem, Menu as MuiMenu, Button, Avatar } from "@mui/material";
import { KeyboardArrowDown } from "@mui/icons-material";
import { NavLink, Link } from "react-router-dom";
import { observer } from "mobx-react-lite";
import { useState } from "react";
import "./NavBar.css";
import {useStore} from "../stores/store.ts";
import * as React from "react"; // We'll create this separately

interface NavItem {
    name: string;
    color: string;
    className: string;
    items: Array<{
        name: string;
        path: string;
        icon: string;
    }>;
}

export default observer(function NavBar() {
    const {
        userStore: { user, logout },
        //recipeStore
    } = useStore();

    const [menuAnchors, setMenuAnchors] = useState<Record<string, HTMLElement | null>>({
        ingredients: null,
        recipes: null,
        dayplans: null,
        user: null,
    });

    const navItems: NavItem[] = [
        {
            name: 'Ingredients',
            color: 'teal',
            className: 'ingredients-dropdown',
            items: [
                { name: 'View Ingredients', path: '/ingredients', icon: 'eye' },
                { name: 'Add Ingredient', path: '/createIngredient', icon: 'plus' }
            ]
        },
        {
            name: 'Recipes',
            color: 'google-plus',
            className: 'recipes-dropdown',
            items: [
                { name: 'View Recipes', path: '/recipes', icon: 'eye' },
                { name: 'Add Recipe', path: '/createRecipe', icon: 'plus' }
            ]
        },
        {
            name: 'DayPlans',
            color: 'pink',
            className: 'dayplans-dropdown',
            items: [
                { name: 'View DayPlans', path: '/dayPlan', icon: 'eye' },
                { name: 'Add DayPlan', path: '/createDayPlan', icon: 'plus' }
            ]
        }
    ];

    const handleMenuOpen = (menuName: string, event: React.MouseEvent<HTMLElement>) => {
        setMenuAnchors({ ...menuAnchors, [menuName]: event.currentTarget });
    };

    const handleMenuClose = (menuName: string) => {
        setMenuAnchors({ ...menuAnchors, [menuName]: null });
    };

    const getColorStyle = (color: string): { backgroundColor: string } => {
        const colorMap: Record<string, string> = {
            'teal': '#00b5ad',
            'google-plus': '#db2828',
            'pink': '#e03997',
        };

        return { backgroundColor: colorMap[color] || '#2185d0' };
    };

/!*
    const getIconElement = (iconName: string) => {
        // This would be better with an icon library that matches your needs
        // For now, just using simple text as placeholder
        return `${iconName === 'eye' ? '👁️' : iconName === 'plus' ? '➕' : ''}`;
    };
*!/

    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="fixed" className="navbar">
                <Container maxWidth="xl">
                    <Toolbar sx={{ display: 'flex', justifyContent: 'space-between', padding: '0.5rem 1rem' }}>
                        {/!* Logo Section *!/}
                        <Box>
                            <MenuItem component={NavLink} to="/" sx={{ display: 'flex', gap: 2 }} className="logo-section">
                                <img src="/assets/logo.png" alt="logo" className="logo" />
                                <Typography variant="h4" fontWeight="bold" className="logo-text">
                                    Dish Craft
                                </Typography>
                            </MenuItem>
                        </Box>

                        {/!* Navigation Buttons *!/}
                        <Box className="nav-buttons-container">
                            {navItems.map((item) => (
                                <Box key={item.name} sx={{ position: 'relative' }}>
                                    <Button
                                        variant="contained"
                                        endIcon={<KeyboardArrowDown />}
                                        onClick={(e) => handleMenuOpen(item.name.toLowerCase(), e)}
                                        className={`nav-dropdown ${item.className}`}
                                        style={getColorStyle(item.color)}
                                    >
                                        {item.name}
                                    </Button>
                                   {/!* <MuiMenu
                                        anchorEl={menuAnchors[item.name.toLowerCase()]}
                                        open={Boolean(menuAnchors[item.name.toLowerCase()])}
                                        onClose={() => handleMenuClose(item.name.toLowerCase())}
                                        className={item.className}
                                        PopoverClasses={{ paper: item.className }}
                                        sx={{
                                            mt: 1,
                                            '& .MuiPaper-root': getColorStyle(item.color)
                                        }}
                                    >
                                        {item.items.map((subItem) => (
                                            <MenuItem
                                                key={subItem.name}
                                                component={Link}
                                                to={subItem.path}
                                                onClick={() => {
                                                    handleMenuClose(item.name.toLowerCase());
                                                    if (subItem.name === 'Add Recipe') {
                                                        recipeStore.clearSelectedRecipe();
                                                    }
                                                }}
                                                sx={{ color: 'white', '&:hover': { backgroundColor: 'rgba(255,255,255,0.1)' } }}
                                            >
                                                <span style={{ marginRight: '0.5rem' }}>{getIconElement(subItem.icon)}</span>
                                                {subItem.name}
                                            </MenuItem>
                                        ))}
                                    </MuiMenu>*!/}
                                </Box>
                            ))}
                        </Box>

                        {/!* User Section *!/}
                        <Box className="user-section">
                            {user ? (
                                <>
                                    <Button
                                        onClick={(e) => handleMenuOpen('user', e)}
                                        className="user-dropdown"
                                        startIcon={
                                            <Avatar
                                                src={user?.image || "/assets/logo.png"}
                                                alt={user.displayName}
                                                className="avatar"
                                            />
                                        }
                                    >
                                        {user.displayName}
                                    </Button>
                                    <MuiMenu
                                        anchorEl={menuAnchors.user}
                                        open={Boolean(menuAnchors.user)}
                                        onClose={() => handleMenuClose('user')}
                                        sx={{ mt: 1 }}
                                    >
                                        <MenuItem
                                            component={Link}
                                            to={`/profile/${user?.username}`}
                                            onClick={() => handleMenuClose('user')}
                                        >
                                            <span style={{ marginRight: '0.5rem' }}>👤</span>
                                            My Profile
                                        </MenuItem>
                                        <MenuItem
                                            onClick={() => {
                                                handleMenuClose('user');
                                                logout();
                                            }}
                                        >
                                            <span style={{ marginRight: '0.5rem' }}>⚡</span>
                                            Logout
                                        </MenuItem>
                                    </MuiMenu>
                                </>
                            ) : (
                                <Box display="flex" gap={2}>
                                    <Button
                                        component={Link}
                                        to="/login"
                                        variant="outlined"
                                        color="inherit"
                                    >
                                        Login
                                    </Button>
                                    <Button
                                        component={Link}
                                        to="/register"
                                        variant="contained"
                                        color="primary"
                                    >
                                        Register
                                    </Button>
                                </Box>
                            )}
                        </Box>
                    </Toolbar>
                </Container>
            </AppBar>
        </Box>
    );
});*/
