<?php

namespace AllyTalks\WebApp\Controller;

use AllyTalks\Assertion\Assertion;
use AllyTalks\Utils\Exception\JsonException;
use AllyTalks\Utils\Exception\SpookyException;
use AllyTalks\WebApp\Application;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\HttpFoundation\Request;

class Registration extends Controller
{
    private $app;
    private $model;

    public function __construct(Application $application)
    {
        $this->app = $application;
        $this->model = $this->app->getModel();
    }

    /**
     * @controller
     *
     * @method POST
     * @route /register/process
     *
     * @param Request $request
     *
     * @return Response
     */
    public function registerNewUserLegacy(Request $request)
    {
        $nickname = $this->prepareData($request->request->get('nickname'));
        $login = $this->prepareData($request->request->get('login'));
        $password = $this->prepareData($request->request->get('password'));
        $email = $this->prepareData($request->request->get('email'));

        $this->assertUserLegacy($nickname, $login, $password, $email);

        $this->model->addUser(['login' => $login, 'password' => $password, 'nickname' => $nickname, 'email' => $email]);

        return new Response('Thank you for your registration!');
    }

    /**
     * @controller
     *
     * @method POST
     * @route /api/register
     *
     * @param Request $request
     *
     * @return JsonResponse
     */
    public function registerNewUserRest(Request $request)
    {
        $data = $request->request->all();

        Assertion::allKeyExists(
            ['login', 'password', 'nickname', 'email'],
            $data,
            'Not all required fields set.'
        );

        Assertion::email($data['email'], 'Incorrect email.');

        try {
            $user = $this->model->addUser($data);
        } catch (SpookyException $e) {
            throw new JsonException('User already exists.');
        }

        return new JsonResponse(['token' => $user->getToken()], 200);
    }

    private function assertUserLegacy($nickname, $login, $password, $email)
    {
        if (empty($nickname) || empty($login) ||
            empty($password) || empty($email) ||
            !filter_var($email, FILTER_VALIDATE_EMAIL)
        ) {
            throw new SpookyException(
                '<h3>Not all fields filled properly! Please check!</h3>
            <a href="/register">Return to Registration</a>'
            );
        }
    }

    private function prepareData($data)
    {
        $data = trim(stripslashes(htmlspecialchars($data)));

        return $data;
    }
}
