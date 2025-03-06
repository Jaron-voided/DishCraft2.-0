import {observer} from "mobx-react-lite";
import { RouterProvider } from "react-router-dom";
import {ToastContainer} from "react-toastify";
import ModalContainer from "../common/modals/ModalContainer.tsx";
import {router} from "../router/Routes.tsx";

function App() {
    //const {commonStore, userStore} = useStore();

  /*  useEffect(() => {
        if (commonStore.token) {
            userStore.getUser().finally(() => commonStore.setAppLoaded())
        } else {
            commonStore.setAppLoaded()
        }
    }, [commonStore, userStore])
*/
    //if (!commonStore.appLoaded) return <LoadingComponent content='Loading app...' />
    return (
        <>
            <ModalContainer />
            <ToastContainer position='bottom-right' hideProgressBar theme='colored' />
            <RouterProvider router={router} />
        </>
    )
}

export default observer(App);