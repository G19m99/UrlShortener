import Card from './components/Card';
import UrlResponse from './components/UrlResponse';
import useCreateShortUrl from "@/hooks/useCreateShortUrl";

const App = () => {
    const { shortenRequest, shortUrl } = useCreateShortUrl();
    return (
        <div className="w-screen h-screen flex flex-col gap-10 justify-center items-center">
            <Card req={shortenRequest} shortUrl={shortUrl} />
            <div className="hidden sm:block">
                <UrlResponse shortUrl={shortUrl} />
            </div>
        </div>
    )
};

export default App;