<?php

namespace AllyTalks\WebApp\Controller;

use AllyTalks\Assertion\Assertion;
use AllyTalks\WebApp\Application;
use Symfony\Component\HttpFoundation\Request;

class UpdateProfile extends Controller
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
     * @route /api/update
     *
     * @param Request $request
     */
    public function updateUserInfo(Request $request)
    {
        $login = $request->request->get('login');
        $password = $this->prepareData($request->request->get('password'));
        $nickname = $this->prepareData($request->request->get('nickname'));
        $email = $this->prepareData($request->request->get('email'));
        //$picture = $request->request->get('picture');

        if($login != null) {
            /*
            if ($picture != null) {
                $picture->move('server/www/userpictures', $login);
            }
            */
            $user = $this->model->getUserByLogin($login);

            if ($password != null && !$user->verifyPassword($password)) {
                $user->setPasswordHash(password_hash($password, PASSWORD_BCRYPT));
            }

            if ($nickname != null && $nickname != $user->getNickname()) {
                $user->setNickname($nickname);
            }

            if ($email != null && filter_var($email, FILTER_VALIDATE_EMAIL)
                && $email != $user->getEmail()
            ) {
                Assertion::email($email, 'Incorrect email.');
                $user->setEmail($email);
            }

            $this->model->initiateFlushing();
        }
    }

    private function prepareData($data)
    {
        $data = trim(stripslashes(htmlspecialchars($data)));

        return $data;
    }
}
