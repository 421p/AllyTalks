<?php
namespace AllyTalks\WebApp\Controller;

use AllyTalks\Utils\Exception\JsonException;
use AllyTalks\WebApp\Model;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\HttpFoundation\Request;

class Auth extends Controller
{
    private $model;

    public function __construct(Model $model)
    {
        $this->model = $model;
    }

    /**
     * @controller
     * @method POST
     * @route /auth
     * @param Request $request
     * @return JsonResponse
     */
    public function authenticationController(Request $request)
    {
        $login = $request->request->get('login');
        $password = $request->request->get('password');

        $user = $this->model->getUserByLogin($login);
        
        if ($user->verifyPassword($password)) {
            $user->generateNewToken();
            $this->model->initiateFlushing();

            return new JsonResponse(['token' => $user->getToken()]);
        } else {
            throw new JsonException('wrong login or password', 401);
        }
    }
}